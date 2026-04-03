# BattleRoyale.py
import tkinter as tk
import random

canva_w, canva_h = 800, 600
sp = 3
run_sp = 20
amount = 20

class MovingObject:
    """Single class that manages the canvas and all moving balls."""
    def __init__(self):
        self.root = tk.Tk()
        self.root.title("Battle Royale")
        self.root.resizable(False, False)
        self.canvas = tk.Canvas(self.root, width=canva_w, height=canva_h, bg="white")
        self.canvas.pack()

        self.objects = []  # list of dicts: {'id', 'x', 'y', 'color'}

        for _ in range(amount):
            x, y, color = self.create()
            id = self.canvas.create_oval(x - 10, y - 10, x + 10, y + 10, fill=color)
            self.objects.append({
                'id': id,
                'x' : x,
                'y': y,
                #'speed': sp,
                'color': color
            })

        self.game_loop()
        self.root.mainloop()

    def create(self):
        x = random.randint(10, canva_w -10)
        y = random.randint(10, canva_h -10)

        color = random.choice([
            "blue",
            "red",
            "green"
        ])
        return x, y, color
    
    def game_loop(self):
        for ball in self.objects:
            self.update_ball(ball)
        self.root.after(run_sp, self.game_loop)

    def update_ball(self, ball):
        id = ball['id']
        x = ball['x']
        y = ball['y']
        color = ball['color']
        #speed = ball['speed']

        dx = 0
        dy = 0

        self.canvas.move(id, dx * sp, dy * sp)
        ball['x'] = x + (dx * sp)
        ball['y'] = y + (dy * sp)
    
    def move_to(id, x, y, target_x, target_y):
        pass

if __name__ == "__main__":
    MovingObject()

# Check contact with circle formula (pi*(r^2))
