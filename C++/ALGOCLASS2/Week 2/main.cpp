#include <iostream>
#include <sstream>
#include <fstream>
#include "Week2.hpp"
#include "../Week 1/Graph.hpp"

void ReadFileGraph(const std::string& _fileName, Graph& _g)
{
	std::string line;
	std::ifstream f;
	f.open(_fileName, std::ios::in);
	if(f.is_open())
	{
		getline(f, line);
		std::stringstream lineStream(line);
		lineStream >> _g.vertexCount;

		Vertex a;
		a.id = 0;
		Vertex b;
		Edge e;

		while(a.id < _g.vertexCount - 1)
		{
			getline(f, line);
			lineStream = std::stringstream(line);
			lineStream >> a.id >> b.id >> e.cost;
			e.a = a;
			e.b = b;
			_g.Edges.push_back(e);
		}
	}
	else std::cout << "Can't open file: " << _fileName;
}

std::vector<Vertex> ReadFileImpGraph(const std::string& _filename)
{
	std::vector<Vertex> vertices;
	std::fstream f;

	f.open(_filename);

	if(f.is_open())
	{
        std::string line;
        getline(f, line);
        std::stringstream linestream(line);

        int count = 0;
        linestream >> count;
        for(int i = 0; i < count; i++)
        {
            getline(f, line);

            Vertex v;
            v.id = i;
            v.data = "";

            for(int i = 0; i < line.length(); i+=2)
                v.data += line[i];
            vertices.push_back(v);
        }
	}

	return vertices;
}

int main(void)
{
	std::cout << "Coursera Algo Class 2 Week 2" << std::endl;
	std::cout << "Dario Seyb 2012" << std::endl;

	Graph g;
	std::cout << "Parsing clustering1.txt" << std::endl;
	ReadFileGraph("clustering1.txt", g);
	std::cout << "Maximum spacing of a 4-clustering:" << std::endl;
	std::cout << kClustering(4, g) << std::endl;

	std::cout << "Parsing clustering2.txt" << std::endl;
	std::vector<Vertex> vertices = ReadFileImpGraph("clustering2.txt");
	std::cout << "Cluster count for a minimum spacing of 3:" << std::endl;
    std::cout << clusterCount(3, vertices) << std::endl;

	fflush(stdin);
	getchar();
	return 0;
}