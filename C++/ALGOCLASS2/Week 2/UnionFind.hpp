#ifndef UNION_FIND_H
#define UNION_FIND_H
#include <unordered_map>

class UnionFind
{
private:
	struct Node
	{
		int id;
		Node* parent;
		int rank;

		inline bool operator == (const Node &o) const {
			return id == o.id;
		}
	};

	std::unordered_map<int, Node*> m_elements;
	Node* Find(Node* _x);
public:
	UnionFind();
	~UnionFind();
	void MakeSet(int _x);
	int Find(int _x);
	void Union(int _a, int _b);
};

#endif