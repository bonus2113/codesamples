#ifndef WEEK_3_H
#define WEEK_3_H
#include <vector>

struct Item
{
	int Weight;
	int Value;
};

int knapsack(std::vector<Item>& _items, int _knapsackSize);
int knapsackRecursion(std::vector<Item>& _items, int _i, int _j);

#endif;