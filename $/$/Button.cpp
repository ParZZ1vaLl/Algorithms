#pragma once

#include <string>
#include <functional>

#include "SFML/Window/Event.hpp"
#include <SFML/Graphics/RectangleShape.hpp>
#include <SFML/Graphics/Text.hpp>

#include "GameObject.cpp"
#include "InterfaceParams.cpp"

class Button : public GameObject
{
private:
	std::string text;
	sf::Color color;
	std::function<void()> on_click;
	sf::Font* font;
	
public:
	Button(sf::Vector2f size, sf::Vector2f position, sf::RenderWindow* window, sf::Font* font):
		GameObject(size, position, window), 
		font(font)
	{}

	virtual bool is_size_fixed() const override
	{
		return false;
	}
	
	void set_text(std::string text)
	{
		this->text = text;
	}

	void set_color(sf::Color color)
	{
		this->color = color;
	}

	void set_on_click_fuction(std::function<void()> on_click)
	{
		this->on_click = on_click;
	}

	// Inherited via GameObject
	virtual void draw() override
	{
		sf::RectangleShape rect;
		rect.setPosition({position.x + 3, position.y + 3});
		rect.setSize({ size.x - 6, size.y - 6});
		rect.setOutlineThickness(3);
		rect.setOutlineColor(color);
		rect.setFillColor(sf::Color::Transparent);

		sf::Text label;
		label.setFont(*font);
		label.setPosition({ position.x + 3, position.y + 3 });
		label.setFillColor(color);
		label.setString(text);

		window->draw(rect);
		window->draw(label);
	}

	virtual void update(sf::Event event, size_t dt) override
	{
		if (event.type == sf::Event::EventType::MouseButtonReleased)
		{
			auto pos = sf::Mouse::getPosition(*window);
			if (pos.x > position.x && pos.y > position.y &&
				pos.x < position.x + size.x && pos.y < position.y + size.y)
			{
				on_click();
			}
		}
	}
};