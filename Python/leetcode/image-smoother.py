#https://leetcode.com/problems/image-smoother
import unittest

class Solution:
    def imageSmoother(self, img: list[list[int]]) -> list[list[int]]:
        rows, columns = len(img), len(img[0])
        result = [[0]*columns for _ in range (rows)]

        for i in range(rows):
            for j in range(columns):
                result[i][j] = self.calculate_avg(img,rows, columns, i, j)

        return result

    def calculate_avg(self,
            img: list[list[int]],
            n_rows: int, 
            n_columns:int,
            row_i: int,
            column_i: int
        ) -> int:

        total_sum = 0
        processed_cells_count = 0
        rows = range(max(0,row_i-1), min(row_i + 2, n_rows))
        columns = range(max(0, column_i-1), min(column_i+2, n_columns))

        for i in rows:
            for j in columns:
                total_sum += img[i][j]
                processed_cells_count += 1

        return int( total_sum / processed_cells_count )


        

            
class Tests(unittest.TestCase):
    def test_single_cell_matrix(self):
        self.assertEqual(Solution().imageSmoother([[1]]), [[1]])

    def test_2_columns_1_row(self):
        self.assertEqual(Solution().imageSmoother([[1, 3]]), [[2, 2]])

    def test_1_example(self):
        self.assertEqual(Solution().imageSmoother([[1,1,1],[1,0,1],[1,1,1]]), [[0,0,0],[0,0,0],[0,0,0]])

    def test_2_example(self):
        self.assertEqual(Solution().imageSmoother(
            [[100,200,100],[200,50,200],[100,200,100]]),
              [[137,141,137],[141,138,141],[137,141,137]])


if __name__ == '__main__':
    unittest.main()