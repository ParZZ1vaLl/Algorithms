#pragma once

#include <SFML/Graphics/Font.hpp>
struct InterfaceParams
{
	sf::Font* font;
	sf::Color enemy_text_color;
	sf::Color player_text_color;
	sf::Color neutral_text_color;
	sf::Color active_button_color;
	sf::Color inactive_button_color;
};