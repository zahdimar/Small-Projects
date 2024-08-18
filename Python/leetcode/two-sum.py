#https://leetcode.com/problems/two-sum/
import unittest

class Solution:
   def twoSum(self, nums: list[int], target: int) -> list[int]:
      l = len(nums)
      if l == 2:
        return [0,1]
      for i in range(l - 1):
        for j in range(i+1,l):
          if nums[i] + nums[j] == target:
            return [i,j]


class Tests(unittest.TestCase):
    def test_empty_array(self):
        self.assertEqual(Solution().twoSum([1, 1], 2), [0,1])


    def test_3_len_array(self):
       self.assertEqual(Solution().twoSum([1,2,3],5), [1,2])

    def test_3_len_array_2(self):
       self.assertEqual(Solution().twoSum([3,2,4],6), [1,2])

    def test_4_len_array_2(self):
       self.assertEqual(Solution().twoSum([2,7,11,15],9), [0,1])




if __name__ == '__main__':
    unittest.main()