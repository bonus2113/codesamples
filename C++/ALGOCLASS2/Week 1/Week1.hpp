#ifndef WEEK_1_H
#define WEEK_1_H
#include <vector>

struct Graph;

struct Job
{
	int Length;
	int Weight;
};

void SchedulingDifference (std::vector<Job>& _jobs);
void SchedulingRatio ( std::vector<Job>& _jobs);

Graph PrimMST(const Graph& _in);

#endif