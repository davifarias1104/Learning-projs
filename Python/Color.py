# Color.py
import pyautogui as py
import threading as th
import keyboard as key
import tkinter as tk
import time
import os

stop_event = th.Event()

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

def stop_listener():
    key.wait("esc")
    stop_event.set()

class Color:
    def __init__(self):
        print("Running...")
        self.root = tk.Tk()
        self.root.title("Color Detector")
        self.root.geometry("300x100")
        self.root.wm_resizable(False, False)
        self.root.protocol("WM_DELETE_WINDOW", self.root.destroy)
        self.is_running = False
        self.root.update()
        self.run_label()

    def run_label(self):
        label = tk.Label(self.root, text="Running.")
        label.pack()

        start_button = tk.Button(self.root, text="Start", command=self.start_)
        start_button.pack()

        stop_button = tk.Button(self.root, text="Stop", command=self.stop_)
        stop_button.pack()

        self.root.update()
        self.root.mainloop()

    def start_(self):
        self.is_running = True
        def a(k):
            def b(e):
                self.is_running = not self.is_running
                return
            return b
        key.on_press_key("=", a("="))
        self.run_()

    def stop_(self):
        self.is_running = False
        self.root.update()

    def run_(self):
        try:
            while self.is_running and self.root.winfo_exists():
                x, y = py.position()
                pixel_color = py.pixel(x, y)
                print(f"{pixel_color} ({x}, {y})")
                #if pixel_color == (75, 219, 106):
                #    py.click()
                #    print("Clicked!")
                #    break
                self.root.update()
                time.sleep(0.5)
        except Exception as e:
            print(f"Error: {e}")

if __name__ == "__main__":
    Clear()
    Color()