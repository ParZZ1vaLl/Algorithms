#pragma once

#include "Stage.cpp"
#include "Minimax.cpp"

class EnemyTurn : public Stage
{
private: 
	GameState& state;
	bool is_end = false;
	Minimax minimax;
	int depth = 3;
public:
	EnemyTurn(const Stage& base, GameState& state) :
		Stage(base), state(state), minimax(state)
	{
		actions.push_back(get_dice_throw_action(minimax.dices_to_throw(state.current_move, depth), state.current_move));
		actions[0].init_action();
	}

	virtual void next() override
	{
		int sum = state.sum(current_action().result()._dices);
		if (sum == 0 || sum + state.round_results[state.current_move] > 15)
		{
			is_end = true;
			state.current_move = (state.current_move + 1) % state.players_count;
			return;
		}
		state.round_results[state.current_move] += sum;
		static_cast<Header*>(objects->at("Header"))->set_round_results(state.round_results);
		if (state.round_results[state.current_move] == 15)
		{
			is_end = true;
			return;
		}
		actions.push_back(get_dice_throw_action(minimax.dices_to_throw(state.current_move, depth), state.current_move));
		Stage::next();
	}

	virtual bool end() override
	{
		return is_end;
	}
};