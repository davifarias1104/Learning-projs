# BenchMark.py
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
import keyboard as key
import pyautogui as py
import time

class benchmark:
    def __init__(self) -> None:
        self.typing()

    def typing(self):

        dr = webdriver.Chrome()
        dr.get("https://humanbenchmark.com/tests/aim")
        time.sleep(1)

        target = dr.find_element(by=By.CLASS_NAME, value="css-7173fr")

        while True:
            target.click()



if __name__ == "__main__":
    benchmark()
