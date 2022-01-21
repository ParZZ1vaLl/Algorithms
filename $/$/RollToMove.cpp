#pragma once

#include "Stage.cpp"

class RollToMove : public Stage
{
private:
	int dices_to_throw = 0;
	int max_dice = 0;
	int min_dice = 7;
	size_t first_turn;
	size_t last_turn;
public:
	RollToMove(const Stage& base, size_t enemy_count) :
		Stage(base)
	{
		actions = std::vector<Action>(2 + enemy_count);
		//игрок бросает кубик для определения очередности ходов
		actions[0] = Action{
			[&]()
			{
				objects->insert(
					{
						"Button_1", new Button(
							{40, 40},
							{(*objects)["Footer"]->get_position().x, (*objects)["Footer"]->get_position().y + 40},
							window,
							font
						)
					}
				);
				auto button1 = static_cast<Button*>((*objects)["Button_1"]);
				button1->set_color(sf::Color::Green);
				button1->set_text("1");
				button1->set_on_click_fuction([&]() {this->dices_to_throw = 1; });
			},
			[&](sf::Event event, size_t dt)
			{
				return dices_to_throw != 0;
			},
			[=]() -> ActionResult
			{
				return ActionResult();
			}
		};
		actions[1] = get_dice_throw_action(1, 0);
		for (int i = 0; i < enemy_count; i++)
		{
			actions[2 + i] = get_dice_throw_action(1, i + 1);
		}

		actions[0].init_action();
	}

	virtual void next() override
	{
		if (current_action().result().type == ActionResult::dices_t)
		{
			if(int(current_action().result()._dices[0]) > max_dice)
			{
				first_turn = current_action_ind - 1;
			}
			else if (int(current_action().result()._dices[0]) < min_dice)
			{
				last_turn = current_action_ind - 1;
			}
		}
		Stage::next();
	}

	size_t get_first_turn() const
	{
		return first_turn;
	}

	size_t get_last_turn() const
	{
		return last_turn;
	}
};