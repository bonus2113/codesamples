#ifndef WEEK_2_H
#define WEEK_2_H
#include <unordered_map>
#include <vector>

struct Graph;
struct Vertex;

int kClustering(int _k, Graph& _g);
int hamDist(const std::string& _a, const std::string& _b);
int clusterCount(int _minDist, std::vector<Vertex>& _vertices);

#endif