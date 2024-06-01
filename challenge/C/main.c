// gcc -o my_program main.c `sdl2-config --cflags --libs` -mavx -lm
// ./my_program 

#include <SDL.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>

const int WINDOW_WIDTH = 800;
const int WINDOW_HEIGHT = 600;
const int DEC_V = 6;
double myTime = 0.0;
int fr = 0;
int fullscreen = 0;  // Переключатель режима полноэкранного режима

// Функция для обновления и рисования пикселей
void draw_pixels(SDL_Renderer *renderer) {
    SDL_SetRenderDrawColor(renderer, 0, 0, 0, SDL_ALPHA_OPAQUE);
    SDL_RenderClear(renderer);

    fr++;
    srand(myTime);

    for (int i = 0; i < 100; i++) {
        double x = (double)rand() / RAND_MAX;
        double y = (double)rand() / RAND_MAX;
        int new_color = rand() % (0x00FFFFFF + 1);

        for (int j = 0; j < 2000; j++) {
            x = fmod(myTime + x + cos(y * 2.2 + x * 0.1), 1.0);
            y = fmod(myTime * 0.3 + y + sin(x * 1.5), 1.0);

            SDL_SetRenderDrawColor(renderer,
                (new_color >> 16) & 0xFF,
                (new_color >> 8) & 0xFF,
                new_color & 0xFF,
                SDL_ALPHA_OPAQUE);

            SDL_RenderDrawPoint(renderer, (int)(x * WINDOW_WIDTH), (int)(y * WINDOW_HEIGHT));
        }
    }

    SDL_RenderPresent(renderer);
}

int main(int argc, char *argv[]) {
    SDL_Window *window;
    SDL_Renderer *renderer;
    SDL_Init(SDL_INIT_VIDEO);

    window = SDL_CreateWindow("SDL Rendering Example",
                              SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,
                              WINDOW_WIDTH, WINDOW_HEIGHT,
                              0);
    renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);

    // Отслеживание FPS
    Uint32 startTicks, frameTicks;
    float fps;
    char windowTitle[100];

    // Основной цикл программы
    int running = 1;
    SDL_Event event;
    while (running) {
        startTicks = SDL_GetTicks();

        while (SDL_PollEvent(&event)) {
            if (event.type == SDL_QUIT) {
                running = 0;
            }
        }

        myTime += 0.0003;
        draw_pixels(renderer);

        // Расчет и вывод FPS
        frameTicks = SDL_GetTicks() - startTicks;
        if (frameTicks > 0) {
            fps = 1000.0f / frameTicks;
            snprintf(windowTitle, 100, "FPS: %.2f", fps);
            SDL_SetWindowTitle(window, windowTitle);
        }

        //SDL_Delay(16); // Цель ~60 FPS
    }

    SDL_DestroyRenderer(renderer);
    SDL_DestroyWindow(window);
    SDL_Quit();
    return 0;
}
