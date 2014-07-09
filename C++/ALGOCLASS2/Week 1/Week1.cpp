#include "Week1.hpp"
#include <algorithm>
#include <iostream>
#include "Graph.hpp"

bool Difference(Job _a, Job _b)
{
	return (_a.Weight - _a.Length > _b.Weight - _b.Length || 
		   (_a.Weight - _a.Length == _b.Weight - _b.Length && _a.Weight > _b.Weight));
}

bool Ratio(Job _a, Job _b)
{
	return (float)_a.Weight/_a.Length > (float)_b.Weight/_b.Length;
}

//Schedule jobs by the difference weight - length (not correct)
void SchedulingDifference ( std::vector<Job>& _jobs)
{
	std::sort(_jobs.begin(), _jobs.end(), Difference);
}

//Schedule jobs by the ratio weight/length (correct)
void SchedulingRatio ( std::vector<Job>& _jobs)
{
	std::sort(_jobs.begin(), _jobs.end(), Ratio);
}

//Returns true if the given Element is in the List
template<class InputIterator, class T>
bool Contains(InputIterator _first, InputIterator _last, const T& _item)
{
	while(_first != _last)
	{
		if(*_first == _item)
			return true;
		++_first;
	}
	return false;
}

//Prims Minimum Spanning Tree Algorithm
Graph PrimMST(const Graph& _in)
{
	std::vector<Vertex> x;
	Vertex s = _in.Edges[0].a;
	x.push_back(s);
	Graph t;
	t.vertexCount = _in.vertexCount;

	//While we haven't added all vertices of _in to the MST
	while(x.size() < _in.vertexCount)
	{
		Edge e;
		Vertex b;
		e.cost = 2147483647;
		//Find shortest edge, that doesn't create a loop in the MST
		for(int i = 0; i < _in.Edges.size(); i++)
		{
			if(_in.Edges[i].cost < e.cost)
			{
				if(Contains(x.begin(), x.end(), _in.Edges[i].a) && 
				  !Contains(x.begin(), x.end(), _in.Edges[i].b))
				{
					e = _in.Edges[i];
					b = e.b;
				}
				else if(Contains(x.begin(), x.end(), _in.Edges[i].b) && 
					   !Contains(x.begin(), x.end(), _in.Edges[i].a))
				{
					e = _in.Edges[i];
					b = e.a;
				}
			}
		}
		//Add it to the MST
		t.Edges.push_back(e);
		x.push_back(b);

		std::cout << "\r";
		std::cout << x.size() << "/" << _in.vertexCount << " Vertices processed" << std::flush;
	}
	std::cout << std::endl;
	return t;
}