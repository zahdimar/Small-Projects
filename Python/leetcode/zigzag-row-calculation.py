# https://leetcode.com/problems/zigzag-conversion
# движение вниз - только столбец
# движение вбок - по диагонали
import math
import unittest

class Solution:
    def get_triangle(self, n:int)-> list[int]:
        length = 2 * n - 1
        result = [None] * length
        middle = n - 1
        result[middle] = middle
        i = 1
        while i < n:
            result[middle-i] = result[middle+i] = n - 1 - i
            i += 1

        return result

    def calc_index(self, *, triange: list[int], rows_num: int, index: int) -> int:
        sequence_length = 2 * rows_num - 2

        effective_index = index % sequence_length
        return triange[effective_index]

    def calculate_row(self, *, index, rows_num):
        if rows_num == 2:
            return index % rows_num
        
        triangle = self.get_triangle(rows_num)
        return self.calc_index(triange=triangle, rows_num=rows_num, index=index)



class TestStringMethods(unittest.TestCase):
    def test_2_row_index_0_result_is_0(self):
        self.assertEqual(Solution().calculate_row(rows_num=2, index=0), 0)

    def test_2_row_index_1_result_is_1(self):
        self.assertEqual(Solution().calculate_row(rows_num=2, index=1), 1)

    def test_2_row_index_2_result_is_0(self):
        self.assertEqual(Solution().calculate_row(rows_num=2, index=2), 0)

    def test_2_row_index_4_result_is_1(self):
        self.assertEqual(Solution().calculate_row(rows_num=2, index=3), 1)
        
    def test_3_row_index_0_result_is_0(self):
        self.assertEqual(Solution().calculate_row(rows_num=3, index=0), 0)
    
    def test_3_row_index_1_result_is_1(self):
        self.assertEqual(Solution().calculate_row(rows_num=3, index=1), 1)

    def test_3_row_index_2_result_is_2(self):
        self.assertEqual(Solution().calculate_row(rows_num=3, index=2), 2)
    
    def test_3_row_index_3_result_is_1(self):
        self.assertEqual(Solution().calculate_row(rows_num=3, index=3), 1)

    def test_3_row_index_4_result_is_1(self):
        self.assertEqual(Solution().calculate_row(rows_num=3, index=4), 0)

    def test_3_row_index_4_result_is_1(self):
        self.assertEqual(Solution().calculate_row(rows_num=3, index=5), 1)
    
    def test_3_row_index_4_result_is_1(self):
        self.assertEqual(Solution().calculate_row(rows_num=7, index=16), 4)

if __name__ == '__main__':
   unittest.main()




