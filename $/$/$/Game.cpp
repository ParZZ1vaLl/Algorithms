#pragma once

#include <map>

#include <SFML/Graphics/RenderWindow.hpp>
#include <SFML/Window/Event.hpp>

#include "GameObject.cpp"
#include "GameState.cpp"
#include "Stage.cpp"
#include "StageManager.cpp"

#include "Button.cpp"

#include "Header.cpp"
#include "DiceAnimation.cpp"
#include "Footer.cpp"

class Game {
private:
	sf::RenderWindow* window;
	sf::Font* font;

	std::map<std::string, GameObject*> objects;

	GameState game_state;

	StageManager* stage_manager;
public:

	Game(size_t players_count)
	{
		std::vector<size_t> round(players_count);
		std::vector<int> game(players_count);
		for (int i = 0; i < players_count; i++)
		{
			round[i] = 0;
			game[i] = 0;
		}
		window = new sf::RenderWindow(sf::VideoMode(500, 500), "AAAAAA");
		game_state = GameState{
			GameStage::roll_to_move,
			players_count,
			0,
			dice::i,
			round,
			game
		};

		font = new sf::Font();
		font->loadFromFile("cour.ttf");

		initialize_objects(players_count);

		Stage base_stage = Stage(window, font, &objects);
		stage_manager = new StageManager(game_state, base_stage);
	}

	sf::RenderWindow* get_render_window()
	{
		return window;
	}

	void update(sf::Event event, size_t dt)
	{
		for (auto object : objects)
		{
			object.second->update(event, dt);
		}
		stage_manager->update(event, dt);
	}
	
	void draw()
	{
		window->clear();
		for (auto object : objects)
		{
			object.second->draw();
		}
		window->display();
	}

	void initialize_objects(size_t players_count)
	{
		objects = std::map<std::string, GameObject*>();

		auto header = new Header(
			window,
			{ 0, 0 },
			players_count,
			font
		);

		header->set_game_results(game_state.game_results);
		header->set_round_results(game_state.round_results);

		objects.insert({ "Header", header });

		auto animation = new DiceAnimation(
			{ 0, header->get_size().y },
			window
		);

		objects.insert({ "Animation", animation });

		auto footer = new Footer(
			{ 0, animation->get_position().y + animation->get_size().y },
			window,
			font
		);

		footer->set_game_results(game_state.game_results[0]);
		footer->set_round_results(game_state.round_results[0]);

		objects.insert({ "Footer", footer });
	}

	~Game()
	{
		delete window;
		delete font;
		for (auto object : objects)
			delete object.second;
		delete stage_manager;
	}
};