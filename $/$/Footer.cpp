#pragma once

#include <SFML/Graphics/Font.hpp>
#include <SFML/Graphics/Text.hpp>

#include "GameObject.cpp"
#include "Button.cpp"

class Footer : public GameObject
{
	size_t round_results;
	int game_results;
	sf::Font* font;

	std::vector<sf::Text*> labels;

	sf::Color throw_dice_button_color = sf::Color::White;
	sf::Color scores_color = sf::Color::Green;
public:
	Footer(sf::Vector2f position, sf::RenderWindow* window,
		sf::Font* font) :
		GameObject(position, window),
		font(font)
	{
		labels = std::vector<sf::Text*>(3);

		labels[0] = new sf::Text("Throw dices", *font);
		labels[0]->setFillColor(throw_dice_button_color);
		labels[0]->setPosition(position);

		labels[1] = new sf::Text(std::to_string(round_results), *font);
		labels[1]->setFillColor(scores_color);
		labels[1]->setPosition({position.x + 200, position.y});

		labels[2] = new sf::Text(std::to_string(game_results), *font);
		labels[2]->setFillColor(scores_color);
		labels[2]->setPosition({ position.x + 250, position.y });
	}

	virtual bool is_size_fixed() const override
	{
		return true;
	}

	void set_round_results(size_t results)
	{
		round_results = results;
		labels[1]->setString(std::to_string(results));
	}

	void set_game_results(int results)
	{
		game_results = results;
		labels[2]->setString(std::to_string(results));
	}

	virtual void draw() override
	{
		for (int i = 0; i < labels.size(); i++)
			window->draw(*labels[i]);
	}

	~Footer()
	{
		for (int i = 0; i < labels.size(); i++)
			delete labels[i];
	}
};