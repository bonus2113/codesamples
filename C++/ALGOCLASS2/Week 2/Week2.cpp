#include "../Week 1/Graph.hpp"
#include <algorithm>
#include "Week2.hpp"
#include "UnionFind.hpp"
#include <iostream>
#include <string>

//The Hamming Distance of two strings
int hamDist(const std::string& _a, const std::string& _b)
{
	int dist = 0;
	for(int i = 0; i < _a.length() && i < _b.length(); i++)
		if(_a[i] == _b[i])
			dist++;

	return dist;
}

bool EdgeCostSort(Edge _a, Edge _b)
{
	return _a.cost < _b.cost;
}

//Splits the graph _g in k clusters and returns the cost of the shortest edge crossing between two of those k clusters, also computes the minimum spanning tree of _g
int kClustering(int _k, Graph& _g)
{
	//Sort the edges in ascending order
	sort(_g.Edges.begin(), _g.Edges.end(), EdgeCostSort);

	UnionFind u;
	//Put each vertex in its own set
	for(int i = 1; i <= _g.vertexCount; i++)
		u.MakeSet(i);

	Graph t;
	//For each edge in _g
	for(int i = 0; i < _g.Edges.size(); i++)
	{
		//Get the current edge (also the lowest cost one out of the edges which weren't processed yet)
		Edge currentEdge = _g.Edges[i];
		//If the edge does not create a loop
		if(u.Find(currentEdge.a.id) != u.Find(currentEdge.b.id))
		{
			//Add it to the MST
			t.Edges.push_back(currentEdge);
			//Unify both endpoints of the edge
			u.Union(currentEdge.a.id, currentEdge.b.id);
		}
	}

	return t.Edges[t.Edges.size() - _k + 1].cost;
}

//Counts the minimum number of clusters so that no two points with a distance of _minDist or less are in different clusters
int clusterCount(int _minDist, std::vector<Vertex>& _vertices)
{
    UnionFind u;
    int unions = _vertices.size();

	//Put each vertex in its own set
    for(int i = 0; i < unions; i++)
		u.MakeSet(_vertices[i].id);

	//For each vertex
    for(int i = 0; i < _vertices.size(); i++)
    {
		//Unify with each other vertex which is not in the same set and has a hamming distance of less than the minimum distance
        for(int j = i + 1; j < _vertices.size(); j++)
        {
            if(u.Find(i) != u.Find(j) && hamDist(_vertices[i].data, _vertices[j].data) < _minDist)
            {
                u.Union(i,j);
				//The total number of unions is one less
                unions--;
            }
        }

		std::cout << "\r";
		std::cout << i << "/" << _vertices.size()  << " Vertices processed" << std::flush;
    }
	std::cout << std::endl;

    return unions;
}