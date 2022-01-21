#pragma once

#include <functional>
#include <map>
#include <iostream>

#include <SFML/Graphics/RenderWindow.hpp>
#include <SFML/Window/Event.hpp>
#include <SFML/Graphics/Font.hpp>

#include "GameObject.cpp"
#include "Button.cpp"
#include "GameState.cpp"
#include "DiceAnimation.cpp"
#include "ActionResult.cpp"
#include "Action.cpp"
#include "Header.cpp"

class Stage
{
protected:
	sf::RenderWindow* window;
	sf::Font* font;
	std::map<std::string, GameObject*>* objects;

	std::vector<Action> actions;
	size_t current_action_ind = 0;
	std::vector<dice> dices = std::vector<dice>(3);
public:

	Stage(sf::RenderWindow* window, sf::Font* font, std::map<std::string, GameObject*>* objects):
		window(window), 
		font(font), 
		objects(objects)
	{}

	//go to next action in stage
	virtual void next()
	{
		current_action_ind++;
		if (!end())
			actions[current_action_ind].init_action();
	}

	//returns true if stage is finished
	virtual bool end()
	{
		return current_action_ind >= actions.size();
	}

	virtual void update(sf::Event event, size_t dt)
	{
		if (end())
			return;

		if (actions[current_action_ind].end_action_predicate(event, dt))
		{
			next();
		}
	}

	virtual Action current_action()
	{
		return actions[current_action_ind];
	}

protected:
	Action get_dice_throw_action(int dices_to_throw, int player_ind)
	{
		return Action{
			[=]() {
				dices.clear();
				for (int i = 0; i < dices_to_throw; i++)
					dices.push_back((dice)(rand() % 6));
				for (int i = 0; i < dices.size(); i++)
				{
					std::cout << (int)dices[i] << " ";
				}
				std::cout << std::endl;
				auto animation = objects->at("Animation");
				static_cast<DiceAnimation*>(animation)->throw_dices(dices);
				if (player_ind > 0)
				{
					auto header = objects->at("Header");
					static_cast<Header*>(header)->unhiglight();
					static_cast<Header*>(header)->higlight_enemy(player_ind - 1);
				}
			},
			[=](sf::Event event, size_t dt) -> bool {
				return event.type == sf::Event::MouseButtonReleased;
			},
			[=]() -> ActionResult {
				auto header = objects->at("Header");
				static_cast<Header*>(header)->unhiglight();
				return ActionResult(dices);
			}
		};
	}
};