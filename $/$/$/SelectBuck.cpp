#pragma once

#include "Stage.cpp"

class SelectBuck : public Stage
{
private:
	bool btn_pressed = false;
	size_t selector;
	dice buck;
public:
	SelectBuck(const Stage& base, size_t selector) :
		Stage(base)
	{
		if (selector == 0)
		{
			actions.push_back(
				Action
				{
					[&]()
					{
						auto button1 = static_cast<Button*>((*objects)["Button_1"]);
						button1->set_on_click_fuction([&]() {this->btn_pressed = true; });
					}, 
					[=](sf::Event event, size_t dt) -> bool
					{
						return this->btn_pressed;
					},
					[=]() -> ActionResult
					{
						return ActionResult();
					}
				}
			);
		}
		actions.push_back(Stage::get_dice_throw_action(1, selector));

		actions[0].init_action();
	}

	virtual void next() override
	{
		if (current_action().result().type == ActionResult::dices_t)
		{
			auto test = current_action().result();
			buck = current_action().result()._dices[0];
			static_cast<Header*>(objects->at("Header"))->set_buck(dices[0]);
		}
		Stage::next();
	}

	dice get_buck() const
	{
		return buck;
	}
};