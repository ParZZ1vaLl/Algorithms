#pragma once

#include "RollToMove.cpp"
#include "SelectBuck.cpp"
#include "PlayerTurn.cpp"
#include "EnemyTurn.cpp"
#include "EndOfGame.cpp"

class StageManager
{
private: 
	GameStage stage;
	GameState& state;
	Stage base;

	RollToMove* roll_to_move = 0;
	SelectBuck* select_buck = 0;
	PlayerTurn* player_turn = 0;
	EnemyTurn* enemy_turn = 0;
	EndOfGame* end_of_game = 0;

public:

	StageManager(GameState& state, Stage& base):
		state(state), 
		base(base)
	{
		stage = GameStage(0);
	}

	void update(sf::Event event, size_t dt)
	{
		size_t selector = -1;
		switch (stage)
		{
		case GameStage::roll_to_move:
			if (!roll_to_move)
				roll_to_move = new RollToMove(base, state.players_count - 1);
			if (roll_to_move->end())
			{
				state.current_move = roll_to_move->get_first_turn();
				selector = roll_to_move->get_last_turn();
				stage = GameStage::buck_selection;
			}
			else
				roll_to_move->update(event, dt);
			break;
		case GameStage::buck_selection:
			if (!select_buck)
				select_buck = new SelectBuck(base, selector);
			if (select_buck->end())
			{
				state.buck = select_buck->get_buck();
				if (state.current_move == 0)
					stage = GameStage::player_turn;
				else
					stage = GameStage::enemy_turn;
			}
			else
				select_buck->update(event, dt);
			break;
		case GameStage::player_turn:
			if (!player_turn)
				player_turn = new PlayerTurn(base, state);
			if (player_turn->end())
			{
				if (state.round_results[0] == 15)
				{
					stage = GameStage::end_of_round;
					break;
				}
				stage = GameStage::enemy_turn;
				enemy_turn = new EnemyTurn(base, state);
			}
			else
				player_turn->update(event, dt);
			break;
		case GameStage::enemy_turn:
			if (!enemy_turn)
				enemy_turn = new EnemyTurn(base, state);
			if (enemy_turn->end())
			{
				if (state.round_results[state.current_move] == 15)
				{
					stage = GameStage::end_of_round;
					break;
				}
				if (state.current_move == 0)
				{
					stage = GameStage::player_turn;
					player_turn = new PlayerTurn(base, state);
				}
				else
				{
					stage = GameStage::enemy_turn;
					enemy_turn = new EnemyTurn(base, state);
				}
			}
			else
				enemy_turn->update(event, dt);
			break;
		case GameStage::end_of_round:
		{
			if (!end_of_game)
				end_of_game = new EndOfGame(base, state);
			end_of_game->update(event, dt);
			break;
		}
		default:
			break;
		}
	}

	~StageManager()
	{
		delete roll_to_move;
		delete select_buck;
		delete player_turn;
		delete enemy_turn;
		delete end_of_game;
	}
};