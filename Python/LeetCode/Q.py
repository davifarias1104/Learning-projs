import os
import time
import random

os.system('cls')
for i in range(101):
    time.sleep(random.randint(0, 3))
    print(f"\r{'\033[92m'}{i}%{'\033[0m'}", end="", flush=True)