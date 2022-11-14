from lib2to3.pytree import type_repr
from typing import Type
import unittest

def soma(a,b):
    if type(a)==int and type(b) == int:
        return a + b
    else:
        raise Type('Tipo incopativel')

#try:
#    assert soma(2,3) == 5
#    print('Soma correta')
#except AssertionError:
#    print('Soma errada')
#
#try:
#    assert soma(2,3) == 2
#    print('OK')
#except AssertionError:
#    print('Soma errada')

class TestSum(unittest.TestCase):
    def test_func_soma(self):
        self.assertEqual(soma(2,3),5)
    def test_excecao_tipos_incompativeis(self):
        self.assertRaises(TypeError,soma,'2',3,6)

if __name__=='__main__':
    unittest.main()