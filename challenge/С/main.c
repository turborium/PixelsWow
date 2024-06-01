// gcc -o my_program main.c `sdl2-config --cflags --libs` -mavx -lm
// ./my_program 

#include <SDL.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>

const int WINDOW_WIDTH = 800;
const int WINDOW_HEIGHT = 600;
double myTime = 0.0;

typedef struct {
    Uint8 r, g, b;
} Color;

SDL_Texture* texture;
Uint32* pixelBuffer;

void draw_pixels(SDL_Renderer *renderer, SDL_Window* window, Uint32* pixelBuffer) {
    memset(pixelBuffer, 0, WINDOW_WIDTH * WINDOW_HEIGHT * sizeof(Uint32));  // Clear buffer

    srand((unsigned int)time(NULL));  // Initialize random seed only once
    for (int i = 0; i < 100; i++) {
        float x = (float)rand() / RAND_MAX;
        float y = (float)rand() / RAND_MAX;
        Color newPixel = {(Uint8)(rand() % 256), (Uint8)(rand() % 256), (Uint8)(rand() % 256)};

        for (int j = 0; j < 2000; j++) {
            x = fmod(myTime + x + cos(y * 2.2f + x * 0.1f), 1.0f);
            y = fmod(myTime * 0.3f + y + sin(x * 1.5f), 1.0f);
            int ix = (int)(x * WINDOW_WIDTH);
            int iy = (int)(y * WINDOW_HEIGHT);
            if (ix >= 0 && ix < WINDOW_WIDTH && iy >= 0 && iy < WINDOW_HEIGHT) {
                int index = iy * WINDOW_WIDTH + ix;
                Uint8 r = fmin(255, pixelBuffer[index] + (newPixel.r / 2));
                Uint8 g = fmin(255, pixelBuffer[index] + (newPixel.g / 2));
                Uint8 b = fmin(255, pixelBuffer[index] + newPixel.b);
                pixelBuffer[index] = SDL_MapRGB(SDL_GetWindowSurface(window)->format, r, g, b);
            }
        }
    }

    SDL_UpdateTexture(texture, NULL, pixelBuffer, WINDOW_WIDTH * sizeof(Uint32));
    SDL_RenderCopy(renderer, texture, NULL, NULL);
    SDL_RenderPresent(renderer);
}

int main(int argc, char *argv[]) {
    SDL_Window *window;
    SDL_Renderer *renderer;
    SDL_Init(SDL_INIT_VIDEO);

    window = SDL_CreateWindow("SDL Particle Simulation",
                              SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,
                              WINDOW_WIDTH, WINDOW_HEIGHT,
                              SDL_WINDOW_SHOWN);
    renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);

    texture = SDL_CreateTexture(renderer, SDL_PIXELFORMAT_ARGB8888, SDL_TEXTUREACCESS_STREAMING, WINDOW_WIDTH, WINDOW_HEIGHT);
    pixelBuffer = (Uint32*)malloc(WINDOW_WIDTH * WINDOW_HEIGHT * sizeof(Uint32));
    if (pixelBuffer == NULL) {
        fprintf(stderr, "Failed to allocate memory for pixels\n");
        SDL_DestroyRenderer(renderer);
        SDL_DestroyWindow(window);
        SDL_Quit();
        return 1;
    }

    Uint32 startTicks, frameTicks;
    float fps;
    char windowTitle[100];
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
        draw_pixels(renderer, window, pixelBuffer);

        frameTicks = SDL_GetTicks() - startTicks;
        if (frameTicks > 0) {
            fps = 1000.0f / frameTicks;
            snprintf(windowTitle, sizeof(windowTitle), "FPS: %.2f", fps);
            SDL_SetWindowTitle(window, windowTitle);
        }

        //SDL_Delay(16);  // Aim for roughly 60 FPS
    }

    free(pixelBuffer);
    SDL_DestroyTexture(texture);
    SDL_DestroyRenderer(renderer);
    SDL_DestroyWindow(window);
    SDL_Quit();
    return 0;
}
