#pragma once

#include <string>

#include <SFML/Graphics/Text.hpp>

#include "GameObject.cpp"
#include "GameState.cpp"

class Header : public GameObject
{
private:
	size_t players_count;
	std::vector<size_t> round_results;
	std::vector<int> game_results;

	sf::Font* font;

	std::vector<std::vector<sf::Text*>> labels;
	sf::Text* buck_label;

	sf::Color text_color = sf::Color::Red;

	static const size_t height = 40;
public:
	Header(sf::RenderWindow* window, sf::Vector2f position, size_t players_count, sf::Font* font):
		GameObject(position, window), 
		players_count(players_count), 
		font(font)
	{
		size = {500, float(height * (players_count - 1)) };
		labels = std::vector<std::vector<sf::Text*>>(players_count - 1);
		for (int i = 0; i < players_count - 1; i++)
		{
			labels[i] = std::vector<sf::Text*>(3);
			for (int j = 0; j < 3; j++)
			{
				labels[i][j] = new sf::Text();
				labels[i][j]->setFont(*font);
				labels[i][j]->setFillColor(text_color);
			}
			labels[i][0]->setPosition({ 0, float(i * height) });
			labels[i][0]->setString("Enemy" + std::to_string(i));

			labels[i][1]->setPosition({200, float(i * height)});

			labels[i][2]->setPosition({ 250, float(i * height) });
		}
		buck_label = new sf::Text();
		buck_label->setFont(*font);
		buck_label->setFillColor(sf::Color::White);
		buck_label->setPosition({ 400, 0 });
	}

	void set_buck(dice buck)
	{
		std::string buck_str = "$ ";
		buck_str.push_back((int)buck + 1 + '0');
		buck_label->setString(buck_str);
	}

	void set_round_results(std::vector<size_t> results)
	{
		round_results = results;
		for (int i = 0; i < players_count - 1; i++)
		{
			labels[i][1]->setString(std::to_string(round_results[i + 1]));
		}
	}

	void set_game_results(std::vector<int> results)
	{
		game_results = results;
		for (int i = 0; i < players_count - 1; i++)
		{
			labels[i][2]->setString(std::to_string(game_results[i + 1]));
		}
	}

	void higlight_enemy(int ind)
	{
		labels[ind][0]->setFillColor(sf::Color::Magenta);
	}

	void unhiglight()
	{
		for (int i = 0; i < players_count - 1; i++)
		{
			labels[i][0]->setFillColor(text_color);
		}
	}

	virtual void draw()
	{
		for (auto row : labels)
		{
			for (auto el : row)
			{
				window->draw(*el);
			}
		}
		window->draw(*buck_label);
	}

	virtual bool is_size_fixed() const override
	{
		return true;
	}

	~Header()
	{
		for (auto row : labels)
		{
			for (auto el : row)
			{
				delete el;
			}
		}
		delete buck_label;
	}
};