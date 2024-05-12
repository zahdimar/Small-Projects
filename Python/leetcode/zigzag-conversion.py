# https://leetcode.com/problems/zigzag-conversion
# Более изящное решние, которое работает медленней...
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
        if rows_num == 2:
            return index % rows_num
        sequence_length = 2 * rows_num - 2
        effective_index = index % sequence_length
        return triange[effective_index]

    def convert(self, s: str, numRows: int) -> str:
        if numRows == 1:
            return s
        
        result = []
        for i in range(numRows):
            result.append("")

        triangle = self.get_triangle(numRows)
        
        for i in range(len(s)):
            index = self.calc_index(triange=triangle, rows_num=numRows, index=i)
            result[index] += (s[i])

        result_string = ""
        for line in result:
            result_string += ''.join(line)
                                     
        return result_string    
    

class TestStringMethods(unittest.TestCase):
    def test_single_letter(self):
        self.assertEqual(Solution().convert("A", 1), "A")

    def test_single_row(self):
        self.assertEqual(Solution().convert("ABCDEF", 1), "ABCDEF")

    def test_2_rows_even(self):
        self.assertEqual(Solution().convert("ABCDEF", 2), "ACEBDF")

    def test_2_rows_odd(self):
        self.assertEqual(Solution().convert("ABCDE", 2), "ACEBD")

    def test_3_rows(self):
        self.assertEqual(Solution().convert("PAYPALISHIRING", 3), "PAHNAPLSIIGYIR")

    def test_4_rows(self):
        self.assertEqual(Solution().convert("PAYPALISHIRING", 4), "PINALSIGYAHRPI")
if __name__ == '__main__':
    unittest.main()