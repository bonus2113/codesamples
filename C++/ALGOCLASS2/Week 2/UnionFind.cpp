#include "UnionFind.hpp"

UnionFind::UnionFind()
{

}

UnionFind::~UnionFind()
{
	for(auto it = m_elements.begin(); it != m_elements.end(); it++)
		delete it->second;

	m_elements.clear();
}


UnionFind::Node* UnionFind::Find(Node* _x)
{
    if(_x->parent != _x)
        _x->parent = Find(_x->parent);
    return _x->parent;
}

int UnionFind::Find(int _x)
{
    return Find(m_elements[_x])->id;
}

void UnionFind::Union(int _x, int _y)
{
    int xRoot = Find(_x);
    int yRoot = Find(_y);

    if(xRoot == yRoot)
        return;

    if(m_elements[xRoot]->rank < m_elements[yRoot]->rank)
        m_elements[xRoot]->parent = m_elements[yRoot];
    else if (m_elements[xRoot]->rank > m_elements[yRoot]->rank)
        m_elements[yRoot]->parent = m_elements[xRoot];
    else
    {
        m_elements[yRoot]->parent = m_elements[xRoot];
        m_elements[xRoot]->rank++;
    }
}

void UnionFind::MakeSet(int _x)
{
    Node* newNode = new Node;
    newNode->id = _x;
    newNode->rank = 0;
    newNode->parent = newNode;
    m_elements[_x]=newNode;
}
