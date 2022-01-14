#pragma once
const int t = 50;
struct BNode {
    int keys[2 * t];
    BNode* children[2 * t + 1];
    BNode* parent;
    int count;
    int countSons;
    bool leaf;
};
class Tree
{
private:
    BNode* root;
    void insert_to_node(int key, BNode* node);
    void sort(BNode* node);
    void restruct(BNode* node);
    void deletenode(BNode* node);
    bool searchKey(int key, BNode* node);
    void remove(int key, BNode* node);
    void removeFromNode(int key, BNode* node);
    void removeLeaf(int key, BNode* node);
    void lconnect(BNode* node, BNode* othernode);
    void rconnect(BNode* node, BNode* othernode);
    void repair(BNode* node);

public:
    Tree();
    ~Tree();
    void insert(int key);
    bool search(int key);
    void remove(int key);
};


