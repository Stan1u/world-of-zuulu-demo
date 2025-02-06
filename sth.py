def funA(n):
    n=3
    print(n)
    if n <= 3:
        funB(n * 2)
    print(n)
 
def funB(n):
    n=3
    funA(n + 1)
    print(n)