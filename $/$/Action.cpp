#pragma once

#include <functional>

#include "SFML/Window/Event.hpp"

#include "ActionResult.cpp"

struct Action
{
	//update objects list to fit current action
	std::function<void(void)> init_action;

	//calls in update() to check if action finished
	std::function<bool(sf::Event event, size_t dt)> end_action_predicate;

	//returns result of the action
	std::function<ActionResult(void)> result;
};