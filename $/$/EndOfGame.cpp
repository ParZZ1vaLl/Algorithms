#pragma once

#include "Stage.cpp"

class EndOfGame : public Stage
{
private:
	std::string message;
	GameState& state;
	sf::Text message_label;
public: 
	EndOfGame(const Stage& base, GameState& state) :
		Stage(base), state(state)
	{
		int i = 0;
		for (; i < state.players_count; i++)
			if (state.round_results[i] == 15)
				break;
		message = "Game Over. ";
		if (i == 0)
			message.append("Player win");
		else
		{
			message.append("Enemy");
			message.push_back(i - 1 + '0');
			message.append(" win");
		}

		actions.push_back(Action
			{
				[&]() {
					for (auto o : *objects)
						delete o.second;
					objects->clear();
					std::cout << message;
					exit(0);
					//message_label = sf::Text();
					//message_label.setFont(*font);
					//message_label.setFillColor(sf::Color::White);
					//message_label.setPosition({ 0, 0 });
					//message_label.setString(message);
				},
				[=](sf::Event event, size_t dt) -> bool {
					std::cout << message;
					exit(0);
					return false;
				},
				[]() -> ActionResult {
					return ActionResult();
				}
			}
		);
	}
};