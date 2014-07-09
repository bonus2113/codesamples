#include <iostream>
#include <sstream>
#include <fstream>
#include <vector>
#include "Week1.hpp"
#include "Graph.hpp"

//Coverts a string to an int using string streams
int StrToInt(const std::string& _s)
{
	int i;
	std::stringstream(_s) >> i;
	return i;
}

void ReadFileJobs(const std::string& _fileName, std::vector<Job>& _jobs)
{
	std::string line;
	std::ifstream f;
	f.open(_fileName, std::ios::in);
	if(f.is_open())
	{
		getline(f, line);
		//read the job count
		int count = StrToInt(line);
		std::stringstream lineStream;

		//for each job in the file
		for(int i = 0; i < count; i++)
		{
			getline(f, line);
			lineStream = std::stringstream(line);
			Job newJob;

			//read the weight and the length
			lineStream >> newJob.Weight >> newJob.Length;
			_jobs.push_back(newJob);
		}
	}
	else std::cout << "Can't open file: " << _fileName;
}

void ReadFileGraph(const std::string& _fileName, Graph& _g)
{
	std::string line;
	std::ifstream f;
	f.open(_fileName, std::ios::in);
	if(f.is_open())
	{
		int edgeCount;
		getline(f, line);
		std::stringstream lineStream(line);

		//read the vertex count
		lineStream >> _g.vertexCount >> edgeCount;

		//for each edge in the file
		for(int i = 0; i < edgeCount; i++)
		{
			getline(f, line);
			lineStream = std::stringstream(line);
			Vertex a;
			Vertex b;
			Edge e;

			//Read the edge information
			lineStream >> a.id >> b.id >> e.cost;
			e.a = a;
			e.b = b;
			_g.Edges.push_back(e);
		}
	}
	else std::cout << "Can't open file: " << _fileName;
}

long long ComputeWeightedCompletionTimes(const std::vector<Job> &_jobs)
{
	long long val = 0;
	long prevTime = 0;
	for (int i = 0; i < _jobs.size(); i++)
    {
		prevTime += _jobs[i].Length;	
		val += prevTime *_jobs[i].Weight;
		if(val < 0)
			return val;
	}
	return val;
}

long long ComputeCost(const Graph &_g)
{
	long long cost = 0;
	for(int i = 0; i < _g.Edges.size(); i++)
		cost += _g.Edges[i].cost;
	return cost;
}

int main(void)
{
	std::cout << "Coursera Algo Class 2 Week 1" << std::endl;
	std::cout << "Dario Seyb 2012" << std::endl;

	std::vector<Job> jobs;
	ReadFileJobs("jobs.txt", jobs);
	std::cout << "Weighted Completion Times:" << std::endl;
	SchedulingDifference (jobs);
	std::cout << "Scheduling by Difference: " << ComputeWeightedCompletionTimes(jobs) << std::endl;
	SchedulingRatio (jobs);
	std::cout << "Scheduling by Ratio:      " << ComputeWeightedCompletionTimes(jobs)<< std::endl;

	Graph g;
	ReadFileGraph("edges.txt", g);
	std::cout << "Prim MST Length:" << std::endl;
	std::cout << ComputeCost(PrimMST(g)) << std::endl;

    fflush(stdin);
	getchar();
	return 0;
}