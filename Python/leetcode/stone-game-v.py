# https://leetcode.com/problems/stone-game-v/
# считает не верно
import math
import unittest

class Solution:

    def stoneGameV(self, stoneValue: list[int]) -> int:
        if len(stoneValue) in [0, 1]:
            return 0

        if len(stoneValue) == 2:
            return min(stoneValue)
        
        sorted_stones = list(sorted(stoneValue))

        left = [sorted_stones.pop()]
        right = []

        while len(sorted_stones) > 0:
            if sum(left) < sum(right) or len(left) == 0:
                left.append(sorted_stones.pop())
                continue
            else:
                right.append(sorted_stones.pop())

        res_list = self.choose_list_to_keep(left, right)
        print(f"-------------\n{left=} = {sum(left)}  {right=} = {sum(right)}")
        print(f"result: {res_list} = {sum(res_list)}")

        return  sum(res_list) + self.stoneGameV(res_list)
    

    def choose_list_to_keep(self, left:list[int], right:list[int]) -> list[int]:
        left_sum = sum(left)
        right_sum = sum(right)

        if (left_sum > right_sum):
            return right
        
        if (left_sum < right_sum):
            return left
        
        # если сумма одинакова -- оставляем самый длинный массив
        return left if len(left) > len(right) else right


class TestStringMethods(unittest.TestCase):
    # def test_singe_element_then_0(self):
    #     self.assertEqual(Solution().stoneGameV([1]), 0)

    # def test_two_elements_then_minimal(self):
    #     self.assertEqual(Solution().stoneGameV([4, 5]), 4)

    # def test_three_elements(self):
    #     print("-=----------------------------------------------------=-")
    #     self.assertEqual(Solution().stoneGameV([6, 2, 3, 4, 5, 5]), 18)

    # def test_1(self):
    #     print("-=----------------------------------------------------=-")
    #     self.assertEqual(Solution().stoneGameV([7,7,7,7,7,7,7]), 28)

    def test_2(self):
        print("-=----------------------------------------------------=-")
        self.assertEqual(Solution().stoneGameV([10,9,8,7,6,5,4,3,2,1]), 37)

    #def test_3(self):
    #    print("-=----------------------------------------------------=-")
    #    self.assertEqual(Solution().stoneGameV([68,75,25,50,34,29,77,1,2,69]), 304)

         

if __name__ == '__main__':
    unittest.main()