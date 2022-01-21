#pragma once

#include "SFML/Graphics/RenderWindow.hpp"
#include "SFML/Window/Event.hpp"

class GameObject
{
protected:
	sf::Vector2f size;
	sf::Vector2f position;
	sf::RenderWindow* window;

	bool dispose = false;
public:
	GameObject(sf::Vector2f size, sf::Vector2f position, sf::RenderWindow* window): 
		size(size), 
		position(position), 
		window(window)
	{}

	GameObject(sf::Vector2f position, sf::RenderWindow* window): 
		position(position), 
		window(window)
	{}

	GameObject(sf::RenderWindow* window) :
		window(window)
	{}

	sf::Vector2f get_size() const
	{
		return size;
	}

	sf::Vector2f get_position() const
	{
		return position;
	}

	void set_position(sf::Vector2f position)
	{
		this->position = position;
	}

	void set_size(sf::Vector2f size)
	{
		if(!this->is_size_fixed())
			this->size = size;
	}

	bool to_dispose() const
	{
		return dispose;
	}
	virtual bool is_size_fixed() const = 0;
	virtual void draw() = 0;
	virtual void update(sf::Event event, size_t dt)
	{}
};