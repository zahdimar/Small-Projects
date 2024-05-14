class Solution:
    def largestLocal(self, grid: list[list[int]]) -> list[list[int]]:

        grid_len = len(grid)
        res_len = grid_len - 2

        result_matrix = [ [None] * (res_len) for i in range(res_len)]

        for res_i in range (res_len):
            for res_j in range(res_len):
                print(f"== == {res_i=} {res_j=} == ==")
                local_max = grid[res_i][res_j]
                for grid_i in [res_i, res_i + 1, res_i + 2]:
                    for grid_j in [res_j, res_j + 1, res_j + 2]:
                        print(f"{grid_i=} x {grid_j=} = {grid[grid_i][grid_j]}")
                        local_max = max(local_max, grid[grid_i][grid_j])
                result_matrix[res_i][res_j] = local_max
                print()

        return result_matrix



print(Solution().largestLocal([[9,9,8,1],[5,6,2,6],[8,2,6,4],[6,2,2,2]]))