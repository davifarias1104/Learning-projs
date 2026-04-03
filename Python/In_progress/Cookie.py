# Cookie.py
import pyautogui as py
import keyboard as key
import time
import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Cookie:
    def __init__(self):
        print("Running...")
        self.clicker()

    def clicker(self):
        def_positions = {
            "space": (207, 375)
        }

        def click(key):
            if key.name in def_positions:
                x, y = def_positions[key.name]
                py.moveTo(x, y)
                mx, my = py.position()
                time.sleep(0.1)
                while (mx, my) == (x, y):
                    py.click(mx, my)
                    time.sleep(0.05)
                    mx, my = py.position()

        for k in def_positions:
            key.on_press_key(k, click)

        key.wait("esc")
         
if __name__ == "__main__":
    Clear()
    Cookie()