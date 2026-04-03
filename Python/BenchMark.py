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
        incomplete = "incomplete"
        letters = "letters"

        dr = webdriver.Chrome()
        dr.get("https://humanbenchmark.com/tests/typing")
        time.sleep(1)

        box = dr.find_element(by=By.CLASS_NAME, value=letters)

        while True:
            i = dr.find_element(by=By.CLASS_NAME, value=incomplete)
            if i != None:
                if i.text == "":
                    box.send_keys(Keys.SPACE)
                else:
                    box.send_keys(i.text)
            else:
                break

class TypeFast:
    def __init__(self) -> None:
        self.typing()

    def typing(self):
        # Set up the WebDriver
        driver = webdriver.Chrome()
        driver.get("https://typefast.io/")
        time.sleep(3)  # Wait for the page and game to load

        try:
            # Click the start button if needed (optional based on page behavior)
            start_button = driver.find_element(By.CLASS_NAME, "start-button")
            start_button.click()
            time.sleep(1)
        except:
            pass  # Ignore if button doesn't exist

        # Main typing loop
        while True:
            try:
                # Get all words that need to be typed (those with class 'word')
                word_elements = driver.find_elements(By.CLASS_NAME, "word")

                for word in word_elements:
                    # Find the active word (highlighted/incomplete one)
                    classes = word.get_attribute("class")
                    if classes and ("current" in classes or "incomplete" in classes):
                        characters = word.find_elements(By.CLASS_NAME, "letter")
                        for char in characters:
                            driver.switch_to.active_element.send_keys(char.text)
                            print(char.text, end="", flush=True)
                        driver.switch_to.active_element.send_keys(Keys.SPACE)
                        print(" ", end="", flush=True)
                        break  # Type only one word per loop iteration

                time.sleep(0.01)  # Small delay to avoid overloading

            except Exception as e:
                print("\nError:", e)
                break

if __name__ == "__main__":
    benchmark()
