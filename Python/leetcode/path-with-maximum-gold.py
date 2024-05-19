#https://leetcode.com/problems/path-with-maximum-gold/
import unittest

class Solution:
   def getMaximumGold(self, grid: list[list[int]]) -> int:
        rows, columns = len(grid), len(grid[0])
        result = 0

        visited = [ [False] * columns for _ in range(rows) ]

        for row in range(rows):
            for column in range(columns):
                if grid[row][column] != 0:
                    result = max(result, self.calculate(grid=grid, current_sum=0, position=(row, column), visited=visited))
        return result
   
   def calculate(self, 
                  grid: list[list[int]], 
                  current_sum: int, 
                  position: tuple[int,int], 
                  visited: list[list[bool]],
                  ) -> int:
        current_row, current_column = position

        max_rows = len(grid)
        max_columns = len(grid[0])

        if (
            current_row < 0 or current_row >= max_rows
            or current_column < 0 or current_column >= max_columns
            or grid[current_row][current_column] == 0
            or visited[current_row][current_column]):
            return current_sum

        current_value = grid[current_row][current_column]
        new_sum = current_value + current_sum

        visited[current_row][current_column] = True

        max_res = max(
            self.calculate(grid, new_sum, (current_row + 1, current_column), visited),
            self.calculate(grid, new_sum, (current_row, current_column + 1), visited),
            self.calculate(grid, new_sum, (current_row - 1, current_column), visited),
            self.calculate(grid, new_sum, (current_row, current_column -1),  visited),
        )
        visited[current_row][current_column] = False

        return max_res
   

class Tests(unittest.TestCase):
    # def test_1(self):
    #     self.assertEqual(Solution().getMaximumGold([[0,6,0],[5,8,7],[0,9,0]]), 24)

    # def test_2(self):
    #     self.assertEqual(Solution().getMaximumGold([[1,0,7],[2,0,6],[3,4,5],[0,3,0],[9,0,20]]), 28)

    def test_3(self):
        self.assertEqual(Solution().getMaximumGold([[12,12,12,1,8],[9,11,15,34,24],[38,0,29,0,28],[3,32,33,21,28],[24,13,2,0,20],[0,24,12,38,0]]), 429)

if __name__ == '__main__':
    unittest.main()