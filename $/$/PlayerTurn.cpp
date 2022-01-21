#pragma once

#include "Stage.cpp"
#include "Footer.cpp"

class PlayerTurn : public Stage
{
private: 
	GameState& state;
	bool btn_pressed = false;
	size_t dices_to_throw;
	bool is_end = false;

	Button* button1;
	Button* button2;
	Button* button3;

	Action dices_to_throw_selection_action = Action
	{
		[&]() {
			button1->set_color(sf::Color::Green);
			button2->set_color(sf::Color::Green);
			button3->set_color(sf::Color::Green);
		},
		[=](sf::Event event, size_t dt) -> bool
		{
			return this->btn_pressed;
		},
		[=]() -> ActionResult
		{
			button1->set_color(sf::Color::Blue);
			button2->set_color(sf::Color::Blue);
			button3->set_color(sf::Color::Blue);
			this->btn_pressed = false;
			return ActionResult();
		}
	};
public:
	PlayerTurn(const Stage& base, GameState& state) :
		Stage(base), state(state)
	{
		if (objects->find("Button_2") == objects->end())
			objects->insert(
				{
					"Button_2", new Button(
						{ 40, 40 },
						{ (*objects)["Footer"]->get_position().x + 40, (*objects)["Footer"]->get_position().y + 40 },
						window,
						font
					) 
				}
			);
		if (objects->find("Button_3") == objects->end())
			objects->insert(
				{
					"Button_3", new Button(
						{ 40, 40 },
						{ (*objects)["Footer"]->get_position().x + 80, (*objects)["Footer"]->get_position().y + 40 },
						window,
						font
					)
				}
			);

		button1 = static_cast<Button*>((*objects)["Button_1"]);
		button2 = static_cast<Button*>((*objects)["Button_2"]);
		button2->set_color(sf::Color::Green);
		button2->set_text("2");
		button3 = static_cast<Button*>((*objects)["Button_3"]);
		button3->set_color(sf::Color::Green);
		button3->set_text("3");

		button1->set_on_click_fuction([&]() {this->dices_to_throw = 1; this->btn_pressed = true; });
		button2->set_on_click_fuction([&]() {this->dices_to_throw = 2; this->btn_pressed = true; });
		button3->set_on_click_fuction([&]() {this->dices_to_throw = 3; this->btn_pressed = true; });
	
		actions.push_back(dices_to_throw_selection_action);
		actions[0].init_action();
	}

	virtual void next() override
	{
		if (current_action().result().type == ActionResult::dices_t)
		{
			int sum = state.sum(current_action().result()._dices);
			if (sum == 0 || sum + state.round_results[0] > 15)
			{
				is_end = true;
				state.current_move = (state.current_move + 1) % state.players_count;

				return;
			}
			state.round_results[0] += sum;
			static_cast<Footer*>(objects->at("Footer"))->set_round_results(state.round_results[0]);
			if (state.round_results[0] == 15)
			{
				is_end = true;
				return;
			}
			actions.push_back(dices_to_throw_selection_action);
		}
		else
		{
			actions.push_back(Stage::get_dice_throw_action(dices_to_throw, 0));
		}
		Stage::next();
	}

	virtual bool end() override
	{
		return is_end;
	}
};