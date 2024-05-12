# https://leetcode.com/problems/median-of-two-sorted-arrays
# solution doesn't match rule O(log (m+n))
import unittest

class Solution:
    def findMedianSortedArrays(self, nums1: list[int], nums2: list[int]) -> float:
        resul_list = list(sorted(nums1 + nums2))
        result_len = len(resul_list)
        if result_len % 2 != 0:
            return resul_list[int((result_len - 1) / 2)]
        
        half_range = int(result_len/2)
        return (resul_list[half_range] + resul_list[half_range - 1]) / 2

class TestStringMethods(unittest.TestCase):

    def test_odd_length(self):
        self.assertEqual(Solution().findMedianSortedArrays([1,3], [2]), 2)

    def test_even_length(self):
        self.assertEqual(Solution().findMedianSortedArrays([1,3], [2, 4]), 2.5)

    def test_negative(self):
        self.assertEqual(Solution().findMedianSortedArrays([-1,-2], [-3, -4]), -2.5)

    def test_empty_array(self):
        self.assertEqual(Solution().findMedianSortedArrays([1], []), 1)

    def test_two_single_arrays(self):
        self.assertEqual(Solution().findMedianSortedArrays([1], [2]), 1.5)
if __name__ == '__main__':
    unittest.main()