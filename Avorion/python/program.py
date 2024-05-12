import json
import os
import pprint

rootDir = 'G:\Documents\GitHub\Small-Projects\Avorion\python'

def formatFactory(factory):
    factoryProduction = ""
    name = factory["factory"]
    level = factory["type"]
    for production in factory["production"]:
        factoryProduction += f"\"{name}\" -> \"{production}\" {levelToStyle[level]}\n"
    for requrement in factory["requre"]:
        factoryProduction += f"\"{requrement}\" -> \"{name}\" {levelToStyle[level]}\n"
    return factoryProduction

levelToStyle = {
    "mine": "[color=gray,style=filled]",
    "basic": "[fillcolor=yellow,color=\"#6d5f01\",style=filled]",
    "low": "[color=green,style=filled]",
    "advanced": "[fillcolor=lightblue,color=blue,style=filled]",
    "hight": "[color=orange,style=filled]"
}

factoriesPerRank = {
    "mine": [],
    "basic": [],
    "low": [],
    "advanced": [],
    "hight": []
}

def resolveColor(factory):
    return levelToStyle[factory["type"]]

resultFile = open(os.path.join(rootDir, 'production.gv'), 'w+')

factoryNodes = ""
resultFile.write("digraph G {\nranksep=2\n")

with open(os.path.join(rootDir, 'factories.json')) as data:
    factories = json.load(data)
    for factory in factories:
        factoryType = factory["type"]
        factoryName = factory['factory']
        factoryPrice = factory['price']
        requres = ";".join(factory["requre"])
        production = ";".join(factory["production"])
        
        factoriesPerRank[factoryType].append(f"\"{factoryName}\"")

        tooltip = f"{factoryName} | {requres} | {production}"

        factoryNodes += f"\"{factoryName}\" {levelToStyle[factoryType]} [label=\"{factoryName}\n{factoryPrice}\";tooltip=\"{tooltip}\";shape=rect]\n"
        resultFile.write(formatFactory(factory))

resultFile.write(factoryNodes)

for sameRankNodes in factoriesPerRank:
    factories = ';'.join(factoriesPerRank[sameRankNodes])
    rank = "rank = min; " if sameRankNodes in ["basic", "mine"] else ""
    resultFile.write(f"{{{rank} {factories}}}\n")

resultFile.write("}")
resultFile.close()