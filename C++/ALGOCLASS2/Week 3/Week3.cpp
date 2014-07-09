#include "Week3.h"
#include <vector>
#include <iostream>
#include <unordered_map>

int Max(int _a, int _b)
{
	if(_a > _b)
		return _a;
	return _b;
}

int knapsack(std::vector<Item>& _items, int _knapsackSize)
{
	std::vector<std::vector<int>> A;
	int n = _items.size();
	for(int i = 0; i <= n; i++)
	{
		A.push_back(std::vector<int>());
		for(int j = 0; j <= _knapsackSize; j++)
		{
			A[i].push_back(0);
		}
	}

	for(int i = 0; i <= _knapsackSize; i++)
		A[0][i] = 0;

	for(int i = 1; i <= n; i++)
	{
		for(int j = 0; j <= _knapsackSize; j++)
		{
			int val1 = A[i-1][j];
			if(j < _items[i-1].Weight)
				A[i][j] = val1;
			else
			{
				int val2 = A[i-1][j - _items[i-1].Weight] + _items[i-1].Value;
				A[i][j] = Max(val1, val2);
			}
		}
	}
	return A[n][_knapsackSize];
}

std::unordered_map<int, std::unordered_map<int, int>> cResults;

void SetValue(int _i, int _j, int _val)
{
	auto f = cResults.find(_i);
	if(f == cResults.end())
		cResults[_i] = std::unordered_map<int, int>();
	cResults[_i][_j] = _val;
}

bool HasValue(int _i, int _j)
{
	auto f = cResults.find(_i);
	if(f == cResults.end())
		return false;

	auto g = cResults[_i].find(_j);
	if(g == cResults[_i].end())
		return false;
	return true;
}

long c = 0;
int knapsackRecursion(std::vector<Item>& _items, int _i, int _j)
{
	if(_i == 0 ||_j == 0)
		return 0;
	if(HasValue(_i, _j))
		return cResults[_i][_j];
	
	int val1;
	val1 = knapsackRecursion(_items, _i - 1, _j);

	if(_j < _items[_i-1].Weight)
	{
		SetValue(_i, _j, val1);
		return val1;
	}
	else
	{
		int val2;
		val2 = knapsackRecursion(_items, _i-1 , _j - _items[_i-1].Weight) + _items[_i-1].Value;
		SetValue(_i, _j, Max(val1, val2));
		return cResults[_i][_j];
	}
}