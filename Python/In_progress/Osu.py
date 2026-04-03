# Osu.py
import pygetwindow as gw
import pyautogui as py
import threading as th
import keyboard as key
import time
import os

stop_event = th.Event()

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

def stop_listener():
    key.wait("esc")
    stop_event.set()

class Osu:
    def __init__(self):
        print("Searching Osu...")
        th.Thread(
            target=stop_listener,
            daemon=True
        ).start()
        self.osu_music: str = self.get_osu_window()
        self.difficult: str = self.osu_music
        print(f"Running: {self.osu_music}")

        while not self.osu_music.endswith("["):
            self.osu_music: str = self.osu_music[:-1]
        self.osu_music: str = self.osu_music[:-2]

        while not self.difficult.startswith("["):
                self.difficult: str = self.difficult[1:]

        self.file: str = self.find_osu_file("C:\\Users\\User\\AppData\\Local\\osu!\\Songs")
        if not self.file:
            print("File not found.")
            return
        
        print(f"File found: {self.file}")

        self.edit_hit_map()
        self.osu_main_loop()

    def get_osu_window(self):
        while True:
            osu_window = gw.getActiveWindow()
            if osu_window and len(osu_window.title) > 4 and osu_window.title.startswith("osu!"):
                osu_music: str = osu_window.title[8:]
                return osu_music
            time.sleep(0.1)
        
    def find_osu_file(self, directory: str):
        try:
            for root, dict, files in os.walk(directory):
                for folder in dict:
                    if self.osu_music in folder:
                        folder_path = os.path.join(root, folder)
                        for root, dict, files in os.walk(folder_path):
                            for file in files:
                                if self.difficult in file:
                                    return os.path.join(root, file)
        except Exception as e:
            print(f"Error: {e}")

    def edit_hit_map(self):
        with open("hit_map.txt", "w") as file_w:
            file_w.write(open(self.file).read())
            
        with open("hit_map.txt", "r") as file_r:
            lines = file_r.readlines()
            for line_num, line in enumerate(lines, 1):
                if "[HitObjects]" in line:
                    line_pop = line_num
                    break
        
        lines = lines[line_pop:]

        with open("hit_map.txt", "w") as file_w:
            file_w.writelines(lines)

    def osu_to_screen(self, screen_w, screen_h):
        scale = min(screen_w / 512, screen_h / 384)

        playfield_w = 512 * scale
        playfield_h = 384 * scale

        offset_x = (screen_w - playfield_w) / 2
        offset_y = (screen_h - playfield_h) / 2

        return offset_x, offset_y, scale
    
    def osu_main_loop(self):
        first = True
        screen_w, screen_h = py.size()
        offset_x, offset_y, scale = self.osu_to_screen(screen_w, screen_h)

        start = time.perf_counter()

        with open("hit_map.txt", "r") as f:
            for line in f:
                if stop_event.is_set():
                    print("Stopped by user")
                    break

                parts = [p.strip() for p in line.split(",")]

                osu_x, osu_y, osu_t = map(int, parts[:3])

                # absolute timing (osu_t assumed milliseconds since start)
                target_time = start + (osu_t / 1000.0)

                # hybrid wait (accurate + low CPU)
                while True:
                    remaining = target_time - time.perf_counter()
                    if remaining <= 0 or stop_event.is_set():
                        break
                    if remaining > 0.002:
                        time.sleep(remaining - 0.001)

                screen_x = offset_x + osu_x * scale
                screen_y = offset_y + osu_y * scale

                if first == True:
                    x, y = py.position()
                    py.moveTo(screen_x, screen_y + 92)
                    time.sleep(1)
                    while True:
                        print(py.pixel(x, y))
                        if py.pixel(x, y) <= (11, 11, 11) and py.pixel(x, y) != (0, 0, 0) or stop_event.is_set():
                            first = False
                            break

                py.moveTo(screen_x, screen_y + 92)
                py.click()
        
if __name__ == "__main__":
    Clear()
    Osu()