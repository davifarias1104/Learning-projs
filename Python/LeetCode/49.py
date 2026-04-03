import os

def Clear():
    os.system('cls' if os.name == 'nt' else 'clear')

class Solution(object):
    def __init__(self) -> None:
        self.returned = self.groupAnagrams(["eat","tea","tan","ate","nat","bat"])
        print(f"key values: {self.returned}")
        
    def groupAnagrams(self, strs):
        #Group anagrams by using the sorted string as a key.
        groups = {}
        for s in strs:
            print(sorted(s))
            print(''.join(sorted(s)))
            key = ''.join(sorted(s))
            groups.setdefault(key, []).append(s)
            print(f"groups: {groups}")
        return list(groups.values())

if __name__ == "__main__":
    Clear()
    Solution()