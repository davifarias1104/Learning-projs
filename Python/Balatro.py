# Balatro.py
import pygetwindow as gw
import pyautogui as py
import keyboard as key
import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Balatro:
    def __init__(self):
        print("Running...")
        self.Key_check()

    def Key_check(self):
        settings = {
            "cards": 8,
            "=": 1,
            "-": -1,
        }

        def_positions = {
            "enter": (585, 675),
            "backspace": (891, 675),
        }

        LEFT_X = 423
        RIGHT_X = 1053
        Y = 510

        def get_card_positions():
            total = settings["cards"]

            if total == 1:
                return {"1": ((LEFT_X + RIGHT_X) // 2, Y)}

            spacing = (RIGHT_X - LEFT_X) // (total - 1)
            return {str(i + 1): (LEFT_X + i * spacing, Y) for i in range(total)}

        def set_key(key):
            def set_config(e):
                match key:
                    case "=":
                        settings["cards"] = min(9, settings["cards"] + settings["="])
                        print(f"Cards increased → {settings['cards']}")
                    case "-":
                        settings["cards"] = max(1, settings["cards"] + settings["-"])
                        print(f"Cards decreased → {settings['cards']}")
                    case _:
                        card_positions = get_card_positions()
                        if key in card_positions:
                            x, y = card_positions[key]
                            if gw.getActiveWindow() and "Balatro" == gw.getActiveWindow().title:
                                py.moveTo(x, y)
                                py.click()
            return set_config

        for i in range(1, 10):
            key.on_press_key(str(i), set_key(str(i)))

        key.on_press_key("=", set_key("="))
        key.on_press_key("-", set_key("-"))

        for k, pos in def_positions.items():
            def make_action(x, y):
                def action(e):
                    if gw.getActiveWindow() and "Balatro" == gw.getActiveWindow().title:
                        py.moveTo(x, y)
                        py.click()
                return action
            key.on_press_key(k, make_action(*pos))

        print("Hotkeys active (press ESC to quit)")
        key.wait("esc")

if __name__ == "__main__":
    Clear()
    Balatro()
