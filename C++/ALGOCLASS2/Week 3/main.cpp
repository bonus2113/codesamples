#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <algorithm>
#include "Week3.h"

int ReadFileKnapsack(const std::string& _filename, std::vector<Item>& _items)
{
	std::ifstream f;
	f.open(_filename);
	if(f.is_open())
	{
		std::string line;
		getline(f, line);
		std::stringstream linestream(line);
		int count;
		int knappsackSize;
		linestream >> knappsackSize >> count;
		for(int i = 0; i < count; i++)
		{
			getline(f, line);
			linestream = std::stringstream(line);
			Item it;
			linestream >> it.Value >> it.Weight;
			_items.push_back(it);
		}
		return knappsackSize;
	}
	else
	{
		std::cout << "Failed to read " << _filename << std::endl;
		return 0;
	}
}

bool ByWeight(Item _a, Item _b)
{
	return _a.Weight < _b.Weight;
}

int getGCD(int _a, int _b)
{
	while(_b != 0)
	{
		int temp = _b;
		_b = _a % _b;
		_a = temp;
	}
	return _a;
}

int findGCD(std::vector<Item>& _items)
{
	if(_items.size() < 2)
		return 1;

	int gcd = getGCD(_items[0].Weight, _items[1].Weight);

	for(int i = 2; i < _items.size() && gcd != 1; i++)
		gcd = getGCD(_items[i].Weight, gcd);

	return gcd;
}

void prepItems(const std::string& filename,  std::vector<Item>& _items, int& _knapsackSize)
{
	_items.clear();
	std::cout << "Parsing " + filename<< std::endl;
	_knapsackSize = ReadFileKnapsack(filename, _items);
	int gcd = findGCD(_items);
	for(int i = 0; i < _items.size(); i++)
		_items[i].Weight /= gcd;
	_knapsackSize /= gcd;
	std::sort(_items.begin(), _items.end(), ByWeight);
}

int main(void)
{
	std::cout << "Coursera Algo Class 2 Week 3" << std::endl;
	std::cout << "Dario Seyb 2013" << std::endl;
	std::vector<Item> items;
	int knapsackSize;

	prepItems("knapsack1.txt", items, knapsackSize);
	std::cout << "Optimal solution: ";
	std::cout << knapsack(items, knapsackSize) << std::endl;

	prepItems("knapsack2.txt", items, knapsackSize);
	std::cout << "Optimal solution: ";
	std::cout << knapsackRecursion(items, items.size(), knapsackSize) << std::endl;

	fflush(stdin);
	getchar();

	return 0;
}