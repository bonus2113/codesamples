#ifndef GRAPH_H
#define GRAPH_H
#include <vector>

struct Vertex
{
	int id;
	std::string data;
	inline bool operator == (const Vertex &o) const {
		return id == o.id;
	}
};

struct Edge
{
	Vertex a;
	Vertex b;
	int cost;
};

struct Graph
{
	int vertexCount;
	std::vector<Edge> Edges;
};

#endif