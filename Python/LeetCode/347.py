from collections import Counter
import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Solution(object):
    def __init__(self) -> None:
        nums = [1,1,1,2,2,3]
        k = 2
        self.returned = self.topKFrequent(nums, k)
        print(self.returned)

    def topKFrequent(self, nums, k):
        keys = []
        count = Counter(nums)
        items = sorted(count.items(), key=lambda item: item[1], reverse=True)[:k]

        for key, value in items:
            keys.append(key)
        return keys

if __name__ == "__main__":
    Clear()
    Solution()