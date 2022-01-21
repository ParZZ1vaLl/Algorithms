#pragma once

#include <stdlib.h>
#include "GameState.cpp"
#include <algorithm>
class Minimax
{
private:
	GameState state;
    int depth;
public:
	Minimax(GameState state) : 
		state(state)
	{}

	float expected_sum(int dices, int current)
	{
		float result = (float)dices / 6.;
		if (dices == 3)
		{
			result += current < 11 ? 5. / 36 : -1. / 36;
			result += current == 0 ? 15. / 216 : - 1. /216;
		}
		return result;
	}

	int dices_to_throw(int player, int depth)
	{
        return minmax(0, player, std::vector<int>()).first;
	}

    std::pair<int, float> minmax(int current_depth,
        int player, std::vector<int> bests) {
        if (current_depth == depth || true)
        {
            auto results = std::vector<std::pair<int, float>>({
                    std::pair<int, float>{1, expected_sum(1, state.round_results[player])},
                    std::pair<int, float>{2, expected_sum(2, state.round_results[player])},
                    std::pair<int, float>{3, expected_sum(3, state.round_results[player])}
                });
            return *std::max_element(results.begin(), results.end(), [](std::pair<int, float> v1, std::pair<int, float> v2) {return v1.second < v2.second; });
        }
        int bestValue = INT_MIN;
        for (int i = 1; i <= 3; i++)
        {
            //пытаемся посчитать значение для 
        }
        //if (max_player) {
        //    int bestValue = INT_MIN;
        //    for (int i = 0; i < depth - 1; i++) {
        //        int val = minmax(current_depth + 1, nodeIndex * 2 + i, false, values, alpha, beta);
        //        bestValue = std::max(bestValue, val);
        //        alpha = std::max(alpha, bestValue);
        //        if (beta <= alpha)
        //            break;
        //    }
        //    return bestValue;
        //}
        //else {
        //    int bestValue = INT_MAX;
        //    for (int i = 0; i < depth - 1; i++) {
        //        int val = minmax(current_depth + 1, nodeIndex * 2 + i, true, values, alpha, beta);
        //        bestValue = std::min(bestValue, val);
        //        beta = std::min(beta, bestValue);
        //        if (beta <= alpha)
        //            break;
        //    }
        //    return bestValue;
        //}
    }
};