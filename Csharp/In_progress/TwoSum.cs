public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        for (int n = 0; n < nums.Length; n++)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (n == i)
                {
                    continue;
                }
                if (nums[n] + nums[i] == target)
                {
                    return new int[] {n, i};
                }
            }
        }
        return null;
    }
}