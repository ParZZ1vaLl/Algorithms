#pragma once

#include <vector>
#include "GameState.cpp"

struct ActionResult
{
	enum ActionResultType {
		dices_t, 
		undefined_t
	} type;

	union
	{
		std::vector<dice> _dices;
	};

	ActionResult() : type(undefined_t)
	{}

	ActionResult(std::vector<dice> dices) :
		_dices(dices), type(dices_t)
	{}

	~ActionResult()
	{
		switch (type)
		{
		case ActionResult::dices_t:
			_dices.~vector();
			return;
		default:
			break;
		}
	}
};