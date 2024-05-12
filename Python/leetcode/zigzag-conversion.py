# https://leetcode.com/problems/zigzag-conversion
# движение вниз - только столбец
# движение вбок - по диагонали
import math
import unittest

class Solution:
    def convert(self, s: str, numRows: int) -> str:
        if numRows == 1:
            return s
        
        result = []
        for i in range(numRows):
            result.append(list(" " * (int(len(s)/2))))

        # current char position
        i_row = 0
        i_column = 0

        #direction
        is_from_top_to_down = True
        
        i = 0
        for i in range(len(s)):
            result[i_row][i_column] = s[i]

            if (is_from_top_to_down and i_row < numRows - 1):
                i_row += 1
                continue
            else:
                is_from_top_to_down = False
            
            if (not is_from_top_to_down and i_row > 0):
                i_row -= 1
                i_column += 1
            else:
                is_from_top_to_down = True
                i_row += 1

        result_string = ""
        for line in result:
            result_string += ''.join(filter(lambda x: x != ' ', line))
                                     
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