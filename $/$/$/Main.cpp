#include <iostream>
#include <chrono>

#include "Game.cpp"

size_t curtime() {
    return std::chrono::duration_cast<std::chrono::milliseconds>(
        std::chrono::system_clock::now().time_since_epoch()
        ).count();
}

int main()
{
    //std::cout << "Enter players count" << std::endl;
    //size_t count;
    //std::cin >> count;

    srand(time(0));

    Game window = Game(3);

    size_t now = curtime();
    while (window.get_render_window()->isOpen())
    {
        sf::Event event;
        while (window.get_render_window()->pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
                window.get_render_window()->close();
            else
            {
                window.update(event, curtime() - now);
                now = curtime();
            }
        }
        window.draw();
    }

    return 0;
}