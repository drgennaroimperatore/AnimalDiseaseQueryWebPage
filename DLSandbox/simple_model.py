import os
from os import chdir
from os import listdir
import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.neighbors import KNeighborsClassifier

from sklearn.tree import DecisionTreeClassifier

import tensorflow as tf # import tensorflow as a whole
from tensorflow import keras # only import Keras

from sklearn.metrics import accuracy_score
from sklearn.metrics import precision_score
from sklearn.metrics import recall_score
from sklearn.metrics import confusion_matrix, ConfusionMatrixDisplay

import matplotlib.pyplot as plt

from keras.layers import Dense, Input
from keras.models import Sequential
from keras.losses import SparseCategoricalCrossentropy
from keras.losses import CategoricalCrossentropy


sheep_disease_name_id_map={

'ZZ_OTHER':14,
'HAEMONCHOSIS':0,
'FASCIOLOSIS':1,
'TRICHOSTRONGIULOSIS':2,
'LUNGWORM':3,
'POX':4,
'PPR':5,
'PASTURELLOSIS':6,
'PASTEUROLLOSIS':6,
'COWDRIOSIS':7,
'MANGE MITE':8,
'NASAL BOT':9,
'COENUROSIS':10,
'CONTAGIOUS ECTHYMA (ORF)':11,
'FASCIOLOSIS':12,
'HYPOCALCEMIA / PREGNANCY TOX.':13,
'HYPOCLCEMIA / PREGNANCY TOX.':13}

curPath = os.getcwd()
rumPath ="Data_Pim\\3_Sheep"
rumPath = os.path.join(curPath, rumPath)
os.chdir(rumPath)

def clean_column_names(originalName):
  return originalName.replace("'","")

def map_id(d):
   return sheep_disease_name_id_map[d]

cattle_csv=pd.DataFrame()
files_to_merge=[]
for f in listdir():
  if f.endswith(".csv") and ("Sheep") in f:
    files_to_merge.append(pd.read_csv(f,encoding="utf-8"))
cattle_csv=pd.concat(files_to_merge, axis=0, ignore_index=True)

signs=(cattle_csv.columns[26:80].tolist())
#cattle_csv['DiseaseChosenByUserNumeric']=cattle_csv['DiseaseChosenByUser'].astype('category').cat.codes
#signs.append('DiseaseChosenByUserNumeric')
#cattle_csv[signs].head()
#print(new_column_names)
#cattle_csv['DiseaseChosenByUserNumeric'][0:5]

eval_data_filename="new_data_sheep.csv"
eval_data=pd.read_csv(eval_data_filename)
eval_data.columns= np.array(list(map(clean_column_names,eval_data.columns)))
print(eval_data.head())

#remove zz_other from eval data
eval_data_no_other=eval_data.loc[eval_data["DiseaseChosenByUser"]!="ZZ_OTHER"]

def preprocess_csv(data):
  data["DiseaseChosenByUserNumeric"] = data["DiseaseChosenByUser"].apply(map_id)

try:
    preprocess_csv(cattle_csv)
    preprocess_csv(eval_data_no_other)
except:
    print(e)

X=cattle_csv[signs]
y=cattle_csv['DiseaseChosenByUserNumeric']

cattle_no_other=cattle_csv.copy()
cattle_no_other_present_signs=cattle_csv.copy()

assert(len(X)==len(y))



