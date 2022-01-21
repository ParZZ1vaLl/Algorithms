#pragma once

#include <vector>
#include <string>

enum class dice { i, ii, iii, iv, v, vi};

enum class GameStage {
	roll_to_move, 
	buck_selection, 
	player_turn, 
	enemy_turn, 
	end_of_round, 
	end_of_game
};

struct GameState
{
	GameStage stage;
	size_t players_count;
	size_t current_move;
	dice buck;
	std::vector<size_t> round_results;
	std::vector<int> game_results;

	int sum(std::vector<dice> dices)
	{
		if (dices.size() == 3 && 
			dices[0] == dices[1] && 
			dices[1] == dices[2])
		{
			if (dices[0] == buck)
				return 15;
			else
				return 5;
		}

		int result = 0;
		for (auto d : dices)
		{
			if (d == buck)
				result++;
		}
		return result;
	}
};