#pragma once

#include "SFML/Window/Event.hpp"
#include "SFML/Graphics/Texture.hpp"
#include "SFML/Graphics/Sprite.hpp"

#include "GameObject.cpp"
#include "GameState.cpp"

class DiceAnimation : public GameObject
{
private:
	std::vector<dice> result = std::vector<dice>();
	sf::Texture dice_texture;
	float dice_width = 129.3f;
	float dice_height = 135.0f;
	sf::Image glass_image;
	std::vector<sf::Sprite> dices;
public:
	DiceAnimation(sf::Vector2f position, sf::RenderWindow* window):
		GameObject(position, window)
	{
		size = { 500, 200 };

		dice_texture = sf::Texture();
		dice_texture.loadFromFile("dice.png");
	}

	void throw_dices(std::vector<dice> result)
	{
		dices = std::vector<sf::Sprite>(result.size());
		for (int i = 0; i < result.size(); i++)
		{
			sf::IntRect rectangle(int(result[i]) * dice_width, 0, dice_width, dice_height);
			dices.push_back(sf::Sprite());
			dices[i].setTexture(dice_texture);
			dices[i].setTextureRect(rectangle);
			dices[i].setPosition({ position.x + i * 150, position.y + 50 });
		}
	}

	virtual bool is_size_fixed() const override
	{
		return true;
	}
	
	virtual void draw() override
	{
		for (int i = 0; i < dices.size(); i++)
		{
			window->draw(dices[i]);
		}
	}

	virtual void update(sf::Event event, size_t dt) override
	{

	}
};