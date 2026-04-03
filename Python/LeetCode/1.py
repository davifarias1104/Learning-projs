import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Solution(object):
    def __init__(self) -> None:
        nums = [2,7,11,15]
        target = 18
        self.returned = self.twoSum(nums, target)
        print(self.returned)

    def twoSum(self, nums, target):
        for i in range(len(nums)-1):
            value = target - nums[i]
            try:
                #search (value) .index starting from i+1
                val = nums.index(value, i+1)
            except:
                val = False
            if val:
                return [i, val]

if __name__ == "__main__":
    Clear()
    Solution()